import { useEffect, useState } from "react";
import PortfolioPicker from "../PortfolioDropdown/PortfolioDropdown";
import "./PortfolioGrid.css";
import type { Portfolio, PortfolioBenchmark, SetPortfolio } from "../../types";
import { fetchAndSetPortfolios } from "../../api/FetchPortfolio";
import { fetchAndSetPortfolioBenchmarks } from "../../api/FetchPortfolioBenchmark";
import DateDropdown from "../DateDropdown/DateDropdown";

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
  const [benchmarks, setBenchmarks] = useState<Portfolio[]>([]);

  const [portfolioBenchmarks, setPortfolioBenchmarks] = useState<
    PortfolioBenchmark[]
  >([]);

  console.log("Portfolios: ", portfolios);
  console.log("Benchmarks: ", benchmarks);
  console.log("Selected portfolio: ", portfolio);
  console.log("Selected benchmark: ", benchmark);
  console.log("Portfolio benchmarks: ", portfolioBenchmarks);

  useEffect(() => {
    if (!portfolio) {
      fetchAndSetPortfolios(setPortfolios);
    }
    if (portfolio) {
      fetchAndSetPortfolioBenchmarks(portfolio, setPortfolioBenchmarks);
    }
  }, [portfolio]);

  useEffect(() => {
    const benchmarks = portfolioBenchmarks.map((pb) => ({
      portfolioId: pb.benchmarkId,
      portfolioName: pb.benchmarkName,
    }));
    setBenchmarks(benchmarks);
  }, [portfolioBenchmarks]);

  return (
    <div className="gridWrapper">
      <div className="cell" id="portfolioDropdown">
        <PortfolioPicker
          portfolio={portfolio}
          portfolios={portfolios}
          setPortfolio={setPortfolio}
        />
      </div>

      <div className="cell" id="benchmarkDropdown">
        <PortfolioPicker
          portfolio={benchmark}
          portfolios={benchmarks}
          setPortfolio={setBenchmark}
        />
      </div>
      <div className="cell" id="dateDropdown">
        <DateDropdown />
      </div>
    </div>
  );
};

export default PortfolioGrid;
