import type { Portfolio } from "../types";
import { api } from "./api";

export async function fetchPortfolios(): Promise<Portfolio[]> {
  try {
    const response = await api("api/Portfolio");
    const data = await response.json();
    console.log("Fetched portfolios: " + data);
    return data;
  } catch (err) {
    console.error(err);
  }
  return [];
}
