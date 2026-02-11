import type { Portfolio, PortfolioBenchmark } from "../types";

export default function createPortfolioBenchmark(
  portfolio: Portfolio,
  benchmark: Portfolio,
): PortfolioBenchmark {
  return {
    portfolioId: portfolio.portfolioId,
    portfolioName: portfolio.portfolioName,
    benchmarkId: benchmark.portfolioId,
    benchmarkName: benchmark.portfolioName,
  };
}
