import type { Portfolio, PortfolioBenchmark, SetPortfolioBenchmarks } from "../types";
import { api } from "./api";

export async function fetchPortfolioBenchmarks(
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

export async function fetchAndSetPortfolioBenchmarks(
  portfolio: Portfolio,
  setPortfolioBenchmarks: SetPortfolioBenchmarks,
) {
  const dtos = await fetchPortfolioBenchmarks(portfolio.portfolioId);
  setPortfolioBenchmarks(dtos);
}
