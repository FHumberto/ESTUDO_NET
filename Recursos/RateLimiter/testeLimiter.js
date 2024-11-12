import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    vus: 3, // 3 usuários virtuais simultâneos
    duration: '20s', // teste por 20 segundos
};

export default function() {
    let res = http.get('https://localhost:7224/WeatherForecast');

    check(res, {
        'status é 200': (r) => r.status === 200,
        'status é 429': (r) => r.status === 429,
        'status é 503': (r) => r.status === 503,
    });

    console.log(`Status Code: ${res.status}`); // Corrige a interpolação de string
    sleep(0.1); // Pausa entre as requisições
}
