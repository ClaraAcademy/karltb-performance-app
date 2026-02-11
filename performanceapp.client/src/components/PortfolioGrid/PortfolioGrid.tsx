import { useEffect, useState } from "react";
import PortfolioPicker from "../PortfolioDropdown/PortfolioDropdown";
import "./PortfolioGrid.css";
import type { Portfolio, PortfolioBenchmark, SetPortfolio } from "../../types";
import { fetchAndSetPortfolios } from "../../api/FetchPortfolio";
import { fetchAndSetPortfolioBenchmarks } from "../../api/FetchPortfolioBenchmark";

interface PortfolioGridProps {
  portfolio: Portfolio | null;
  setPortfolio: SetPortfolio;
  benchmark: Portfolio | null;
  setBenchmark: SetPortfolio;
}

const PortfolioGrid = ({
  portfolio,
  setPortfolio,
  benchmark,
  setBenchmark,
}: PortfolioGridProps) => {
  const [portfolios, setPortfolios] = useState<Portfolio[]>([]);
  const [portfolioBenchmarks, setPortfolioBenchmarks] = useState<
    PortfolioBenchmark[]
  >([]);

  useEffect(() => {
    fetchAndSetPortfolios(setPortfolios);
  }, []);

  useEffect(() => {
    if (portfolio) {
      fetchAndSetPortfolioBenchmarks(portfolio, setPortfolioBenchmarks);
    } else {
      setPortfolioBenchmarks([]);
    }
  }, [portfolio]);

  const benchmarks = portfolioBenchmarks.map((pb) => ({
    portfolioId: pb.benchmarkId,
    portfolioName: pb.benchmarkName,
  }));

  return (
    <div className="gridWrapper">
      <div className="cell" id="portfolioDropdown">
        <PortfolioPicker
          label={"Portfolio"}
          portfolio={portfolio}
          portfolios={portfolios}
          setPortfolio={setPortfolio}
        />
      </div>
      <div className="cell" id="benchmarkDropdown">
        <PortfolioPicker
          label={"Benchmark"}
          portfolio={benchmark}
          portfolios={benchmarks}
          setPortfolio={setBenchmark}
        />
      </div>
    </div>
  );
};

export default PortfolioGrid;
