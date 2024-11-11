import { ICityWeather } from './Api';
import styles from './CityWeather.module.css';
import { useCallback } from 'react';

interface CityWeatherProps {
    cityWeather: ICityWeather;
    onRemoveCity: (locationKey: string) => void;
}

export function CityWeather(props: CityWeatherProps) {
    var cityWeather = props.cityWeather;
    const handleRemoveCity = useCallback(() => props.onRemoveCity(cityWeather.locationKey), [cityWeather.locationKey]);

    return (
        <div className={styles.cityWeather}>
            <div className={styles.city}>{cityWeather.cityName}</div>
            <div className={styles.precipitation}>{cityWeather.precipitation}</div>
            <div className={styles['highest-temprature']}>{cityWeather.highestDailyTemprature}</div>
            <div className={styles['lowest-temprature']}>{cityWeather.lowestDailyTemprature}</div>
            <button className={styles['remove-button']} onClick={handleRemoveCity}>Remove</button>
        </div>
    );
}