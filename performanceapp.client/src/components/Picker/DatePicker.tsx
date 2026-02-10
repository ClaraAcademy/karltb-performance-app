import { useEffect, useState } from "react";
import type { SetDate } from "../../types";
import Picker from "./Picker";
import { createKeyValuesFromDates } from "../../Factories/KeyValueFactory";
import { fetchAndSetDates } from "../../api/FetchDates";

interface DatePickerProps {
  date: Date | null;
  setDate: SetDate;
}

export default function DatePicker({ date, setDate }: DatePickerProps) {
  const [selected, setSelected] = useState<string>("");
  const [dates, setDates] = useState<Date[]>([]);

  useEffect(() => {
    fetchAndSetDates(setDates);
  }, []);

  useEffect(() => {
    if (date) {
      setSelected(date.getTime().toString());
    } else {
      setSelected("");
    }
  }, [date]);

  const onChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const time = Number(e.target.value);
    const date = dates.find((d) => d.getTime() === time) ?? null;
    setDate(date);
    setSelected(date ? date.getTime().toString() : "");
  };

  const values = createKeyValuesFromDates(dates);

  return (
    <Picker
      selected={selected}
      onChange={onChange}
      values={values}
      placeholder={"Choose a date"}
    />
  );
}
