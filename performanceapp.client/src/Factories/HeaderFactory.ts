import type {
  Portfolio,
  PortfolioBenchmark,
  PositionColumn,
  Row,
} from "../types";
import createPortfolioBenchmark from "./PortfolioBenchmarkFactory";

export function createHeaderForPositions<T>(
  columns: PositionColumn<T>[],
  name: string,
): Row {
  const values = columns.map((col, i) => ({
    key: col.className + "-header-" + i,
    value: col.header,
  }));
  const key = name.toLowerCase().replace(/\s+/g, "-") + "-header";
  return {
    key: key,
    values: values,
  };
}

export function createHeaderForPortfolioBenchmark(
  portfolioBenchmark: PortfolioBenchmark,
): Row {
  return {
    key: "keyFigureName-header",
    values: [
      {
        key: "keyFigureName-header",
        value: "Key Figure",
      },
      {
        key: portfolioBenchmark.portfolioId.toString(),
        value: portfolioBenchmark.portfolioName,
      },
      {
        key: portfolioBenchmark.benchmarkId.toString(),
        value: portfolioBenchmark.benchmarkName,
      },
    ],
  };
}

export function createHeaderForPortfolioAndBenchmark(
  portfolio: Portfolio,
  benchmark: Portfolio,
): Row {
  const portfolioBenchmark = createPortfolioBenchmark(portfolio, benchmark);
  return createHeaderForPortfolioBenchmark(portfolioBenchmark);
}
