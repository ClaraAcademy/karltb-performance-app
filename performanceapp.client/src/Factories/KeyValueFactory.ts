import type { KeyValue, Portfolio } from "../types";

export function createKeyValue(
  key: string,
  value: string,
): KeyValue<string, string> {
  return { key: key, value: value };
}

export function createKeyValueFromPortfolio(
  portfolio: Portfolio,
): KeyValue<string, string> {
  return {
    key: portfolio.portfolioId.toString(),
    value: portfolio.portfolioName,
  };
}

export function createKeyValuesFromPortfolios(
  portfolios: Portfolio[],
): KeyValue<string, string>[] {
  return portfolios.map((p) => createKeyValueFromPortfolio(p));
}
