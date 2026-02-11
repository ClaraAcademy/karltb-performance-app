import type { PortfolioBenchmarkKeyFigure, SetKeyFigures } from "../types";
import { api } from "./api";

export default async function fetchKeyFigures(
  portfolioId: number,
): Promise<PortfolioBenchmarkKeyFigure[]> {
  try {
    const endpoint = `api/performance?portfolioId=${portfolioId}`;
    const response = await api(endpoint);
    const data = await response.json();
    console.log("Fetched portfolioBenchmarkKeyFigure: " + data);
    return data;
  } catch (err) {
    console.error(err);
  }
  return [];
}

export async function fetchAndSetKeyFigures(
  portfolioId: number,
  setKeyFigures: SetKeyFigures,
) {
  const keyFigures = await fetchKeyFigures(portfolioId);
  setKeyFigures(keyFigures);
}
