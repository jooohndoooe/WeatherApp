import styles from './WeatherClient.module.css'
import { CityWeather } from "./Api";
import Navbar from "./Navbar";
import { CityTable } from "./CityTable";
import { Alert } from "./Alert";

interface WetherClientProps {
    cityWeathers: CityWeather[];
    onAddCity: (cityName: string) => void;
    onRemoveCity: (locationKey: string) => void;
}

export function WeatherClient(props: WetherClientProps) {
    return (
        <div className={styles['weather-client']}>
            <div className={styles.navbar}>
                <Navbar onAddCity={props.onAddCity} />
            </div>
            <div className={styles.alert}>
                <Alert cityWeathers={props.cityWeathers} />
            </div>
            <div className={styles['city-table']}>
                <CityTable cityWeathers={props.cityWeathers} onRemoveCity={props.onRemoveCity} />
            </div>
        </div>
    );
}