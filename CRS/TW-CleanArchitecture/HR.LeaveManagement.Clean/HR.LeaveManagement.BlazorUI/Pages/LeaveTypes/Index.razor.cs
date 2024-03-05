using Blazored.Toast.Services;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveTypes;

public partial class Index
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public ILeaveTypeService LeaveTypeService { get; set; }

    [Inject]
    public ILeaveAllocationService LeaveAllocationsService { get; set; }

    [Inject]
    IToastService toastService { get; set; }

    public List<LeaveTypeVM> LeaveTypes { get; private set; }
    public string Message { get; set; } = string.Empty;

    protected void CreateLeaveType()
    {
        NavigationManager.NavigateTo("/leavetypes/create/");
    }

    protected void AllocateLeaveType(int id)
    {
        LeaveAllocationsService.CreateLeaveAllocations(id);
    }

    protected void EditLeaveType(int id)
    {
        NavigationManager.NavigateTo($"/leavetypes/edit/{id}");
    }

    protected void DetailsLeaveType(int id)
    {
        NavigationManager.NavigateTo($"/leavetypes/details/{id}");
    }

    protected async Task DeleteLeaveType(int id)
    {
        Services.Base.Response<Guid> response = await LeaveTypeService.DeleteLeaveType(id);

        if (response.Success)
        {
            //? re-renderiza o componente
            //StateHasChanged();
            toastService.ShowSuccess("Leave Type deleted Successfully");
            await OnInitializedAsync();
        }
        else
        {
            Message = response.Message;
        }
    }

    protected override async Task OnInitializedAsync()
    {
        //? fazendo o overwrite, para preencher a lista na tabela
        LeaveTypes = await LeaveTypeService.GetLeaveTypes();
    }
}