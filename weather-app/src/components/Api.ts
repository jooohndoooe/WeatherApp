export interface ICityWeather {
    locationKey: string;
    cityName: string;
    precipitation: string;
    highestDailyTemprature: number;
    lowestDailyTemprature: number;
    showNotification: boolean;
}

export class CityWeather implements ICityWeather {
    locationKey: string;
    cityName: string;
    precipitation: string;
    highestDailyTemprature: number;
    lowestDailyTemprature: number;
    showNotification: boolean;

    constructor() {
        this.locationKey = "";
        this.cityName = "";
        this.precipitation = "";
        this.highestDailyTemprature = 0;
        this.lowestDailyTemprature = 0;
        this.showNotification = false;
    }
}

export async function loadCityWeather(): Promise<CityWeather[]> {
    const result = await (await fetch("/api/weather")).json();
    return result;
}

export async function addCity(city: string) {
    await fetch('/api/city', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ city })
    });
}

export async function removeCity(locationKey: string) {
    await fetch('/api/city/' + locationKey, { method: 'DELETE' });
}