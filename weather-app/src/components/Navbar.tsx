import styles from './Navbar.module.css';
import { useCallback, useState } from 'react';

interface NavbarProps {
    onAddCity: (cityName: string) => void;
}

export function Navbar(props: NavbarProps) {
    const [city, setCity] = useState("");

    const handleAddCity = useCallback(() => props.onAddCity(city), [city]);

    return (
        <div className={styles.navbar}>
            <div className={styles['app-name']}>WeatherApp</div>
            <label>
                Text input: <input onChange={e => setCity(e.currentTarget.value)} />
            </label>
            <button className={styles['button-container']} onClick={handleAddCity}>
                Add City
            </button>
        </div>
    );
}

export default Navbar;