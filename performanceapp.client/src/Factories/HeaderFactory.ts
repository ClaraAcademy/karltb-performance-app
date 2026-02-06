import type { PortfolioBenchmark, Row } from "../types";

export function createHeader(portfolioBenchmark: PortfolioBenchmark): Row {
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
