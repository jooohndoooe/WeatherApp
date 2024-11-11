import './App.css';
import { addCity, CityWeather, loadCityWeather, removeCity } from "./components/Api";
import { WeatherClient } from './components/WeatherClient';
import { useEffect, useState, useCallback } from "react";

function App() {
    const [cityWeather, setCityWeather] = useState(new Array<CityWeather>());

    useEffect(
        () => {
            async function loadState() {
                const result = await loadCityWeather();
                setCityWeather(result);
            }
            loadState();
            setInterval(loadState, 10000);
        }, []);

    const handleAddCity = useCallback((cityName: string) => {
        addCity(cityName).then(() => {
            loadCityWeather().then(result => { setCityWeather(result) });
        });
    }, [setCityWeather]);

    const handleRemoveCity = useCallback((locationKey: string) => {
        removeCity(locationKey).then(() => {
            loadCityWeather().then(result => { setCityWeather(result) });
        });
    }, [setCityWeather]);

    return (
        <div className="App">
            <WeatherClient cityWeathers={cityWeather} onAddCity={handleAddCity} onRemoveCity={handleRemoveCity} />
        </div>
    );
}

export default App;
