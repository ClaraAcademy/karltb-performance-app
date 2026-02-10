import type { DateInfo, SetDates } from "../types";
import { api } from "./api";

export async function fetchDates(): Promise<Date[]> {
  try {
    const response = await api("api/DateInfo");
    const data: DateInfo[] = await response.json();
    const dates = data.map((d) => new Date(d.bankday));
    console.log("Fetched dates: " + dates);
    return dates;
  } catch (err) {
    console.error(err);
  }
  return [];
}

export async function fetchAndSetDates(setDates: SetDates) {
  const dates = await fetchDates();
  setDates(dates);
}
