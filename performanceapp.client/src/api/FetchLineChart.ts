import type { SetSvg } from "../types";
import { api } from "./api";

export async function fetchLineChart(
  portfolioId: number,
  width: number,
  height: number,
): Promise<string> {
  try {
    const endpoint = `api/svg?portfolioId=${portfolioId}&width=${width}&height=${height}`;
    const response = await api(endpoint);
    const data = await response.text();
    const encoded = "data:image/svg+xml;base64," + btoa(data);
    return encoded;
  } catch (err) {
    console.error(err);
  }
  return "";
}

export async function fetchAndSetLineChart(
  portfolioId: number,
  width: number,
  height: number,
  setSvg: SetSvg,
) {
  const svg = await fetchLineChart(portfolioId, width, height);
  setSvg(svg);
}
