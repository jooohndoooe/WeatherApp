import styles from "./Alert.module.css";
import { CityWeather } from "./Api";

interface AlertProps {
    cityWeathers: CityWeather[]
}

export function Alert(props: AlertProps) {
    var citiesToAlert = ""
    props.cityWeathers.forEach(cityWeather => {
        if (cityWeather.showNotification) {
            citiesToAlert += cityWeather.cityName + " ";
        }
    })

    var alertVisibility = 'visible';
    if (citiesToAlert == "") {
        alertVisibility = 'hidden';
    }

    type Visibility = 'visible' | 'hidden' | 'collapse' | undefined;
    return (
        <div className={styles.alert} style={{ visibility: alertVisibility as Visibility }}>
            {citiesToAlert} will have rain today
        </div>
    )
}