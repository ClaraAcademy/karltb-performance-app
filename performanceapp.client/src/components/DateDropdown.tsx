import { useState, useEffect } from "react";
import type { DateInfo } from "../types";
import { useBankday } from "../contexts/BankdayContext";
import { api } from "../api/api";

interface DateDropdownProps {
  onSelect?: (date: Date | null) => void;
}

const DateDropdown: React.FC<DateDropdownProps> = ({ onSelect }) => {
  const [dates, setDates] = useState<Date[]>([]);
  const { bankday, setBankday } = useBankday();

  const fetchDates = async () => {
    try {
      const response = await api("api/DateInfo");
      const data: DateInfo[] = await response.json();
      setDates(data.map((d) => new Date(d.bankday)));
    } catch (err) {
      console.error(err);
    }
  };

  useEffect(() => {
    fetchDates();
  }, []);

  const formatDate = (d: Date) => {
    return d.toISOString().split("T")[0];
  };

  const handleChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const time = Number(e.target.value);
    const selectedDate = dates.find((d) => d.getTime() === time) ?? null;
    setBankday(selectedDate);
    if (onSelect) onSelect(selectedDate);
  };

  return (
    <select value={bankday ? bankday.getTime() : ""} onChange={handleChange}>
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
