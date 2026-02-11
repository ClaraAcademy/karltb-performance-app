import type { Key } from "react";
import type { KeyValue, Portfolio } from "../types";
import formatDate from "../formatters/FormatDate";

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

export function createKeyValueFromDate(date: Date): KeyValue<string, string> {
  return {
    key: date.getTime().toString(),
    value: formatDate(date),
  };
}

export function createKeyValuesFromDates(
  dates: Date[],
): KeyValue<string, string>[] {
  return dates.map((d) => createKeyValueFromDate(d));
}
