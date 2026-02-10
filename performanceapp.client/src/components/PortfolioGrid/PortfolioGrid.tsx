import { useEffect } from "react";
import { usePortfolioBenchmark } from "../../contexts/PortfolioBenchmarkContext";
import PortfolioPicker from "../PortfolioDropdown/PortfolioDropdown";
import "./PortfolioGrid.css";
import DateDropdown from "../DateDropdown/DateDropdown";
import type { Portfolio } from "../../types";
import fetchPortfolioBenchmarks from "../../api/FetchPortfolioBenchmark";
import PortfolioInfo from "./PortfolioInfo";

interface PortfolioGridProps {
  portfolio: Portfolio | null;
  setPortfolio: (portfolio: Portfolio | null) => void;
}

const PortfolioGrid = (props: PortfolioGridProps) => {
  const { portfolio, setPortfolio } = props;
  const { portfolioBenchmark, setPortfolioBenchmark } = usePortfolioBenchmark();

  useEffect(() => {
    async function fetchAndSetPortfolioBenchmark() {
      if (portfolio != null) {
        const dtos = await fetchPortfolioBenchmarks(portfolio.portfolioId);
        setPortfolioBenchmark(dtos);
      }
    }
    fetchAndSetPortfolioBenchmark();
  }, [portfolio]);

  const portfolioName = portfolioBenchmark
    ? portfolioBenchmark[0].portfolioName
    : "";

  const benchmarkName = portfolioBenchmark
    ? portfolioBenchmark[0].benchmarkName
    : "";

  return (
    <div className="gridWrapper">
      <div className="cell" id="portfolioDropdown">
        <PortfolioPicker portfolio={portfolio} setPortfolio={setPortfolio} />
      </div>
      <div className="cell" id="dateDropdown">
        <DateDropdown />
      </div>
      <PortfolioInfo header="Selected Portfolio" name={portfolioName} />
      <PortfolioInfo header="Selected Benchmark" name={benchmarkName} />
    </div>
  );
};

export default PortfolioGrid;
