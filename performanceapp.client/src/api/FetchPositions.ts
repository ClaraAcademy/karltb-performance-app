import { api } from "./api";

const getDateString = (date: Date | null) => {
  return date ? date.toISOString().split("T")[0] : "";
};

export default async function fetchPositions(
  endpoint: string,
  portfolioId: number,
  date: Date,
) {
  const dateString = getDateString(date);
  const url = `api/position/${endpoint}?portfolioId=${portfolioId}&date=${dateString}`;
  try {
    const response = await api(url);
    const data = await response.json();
    return data;
  } catch (error) {
    console.error(error);
  }
  return [];
}
