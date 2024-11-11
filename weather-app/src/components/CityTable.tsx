import { ICityWeather } from './Api'
import { CityWeather } from './CityWeather';

interface CityTableProps {
    cityWeathers: ICityWeather[];
    onRemoveCity: (locationKey: string) => void;
}

export function CityTable(props: CityTableProps) {
    const cityTable = props.cityWeathers.map(_cityWeather =>
        <div key={_cityWeather.locationKey}>
            <CityWeather cityWeather={_cityWeather} onRemoveCity={props.onRemoveCity} />
        </div>
    )
    return (<div>{cityTable}</div>)
}