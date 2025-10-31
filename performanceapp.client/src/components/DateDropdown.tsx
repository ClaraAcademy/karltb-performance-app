import { useState, useEffect } from "react";
import type { DateInfo } from "../types";

interface DateDropdownProps {
    onSelect?: (date: Date | null) => void;
}

const DateDropdown: React.FC<DateDropdownProps> = ({ onSelect }) => {
    const [dates, setDates] = useState<Date[]>([]);
    const [selected, setSelected] = useState<Date | null>(null);

    useEffect(() => {
        fetch("api/DateInfo")
            .then((res) => res.json())
            .then((data: DateInfo[]) =>
                setDates(data.map((d) => new Date(d.bankday))),
            )
            .catch((err) => console.error(err));
    }, []);

    const formatDate = (d: Date) => {
        return d.toISOString().split("T")[0];
    };

    const handleChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        const time = Number(e.target.value);
        const selectedDate = dates.find((d) => d.getTime() === time) ?? null;
        setSelected(selectedDate);
        if (onSelect) onSelect(selectedDate);
    };

    return (
        <select
            value={selected ? selected.getTime() : ""}
            onChange={handleChange}
        >
            {/* Placeholder that dissapears after choice */}
            <option value="" disabled hidden>
                Choose a date
            </option>
            {dates.map((d) => (
                <option key={d.getTime()} value={d.getTime()}>
                    {formatDate(d)}
                </option>
            ))}
        </select>
    );
};

export default DateDropdown;
