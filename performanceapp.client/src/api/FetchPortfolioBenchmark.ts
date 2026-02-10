import type { PortfolioBenchmark } from "../types";
import { api } from "./api";

export default async function fetchPortfolioBenchmarks(
  portfolioId: number,
): Promise<PortfolioBenchmark[]> {
  try {
    const endpoint = `api/PortfolioBenchmark?portfolioId=${portfolioId}`;
    const response = await api(endpoint);
    const data = await response.json();
    console.log("Fetched portfolioBenchmarks: " + data);
    return data;
  } catch (err) {
    console.error(err);
  }
  return [];
}
