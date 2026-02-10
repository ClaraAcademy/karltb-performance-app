import { useEffect } from "react";
import { usePortfolioBenchmark } from "../../contexts/PortfolioBenchmarkContext";
import PortfolioPicker from "../PortfolioDropdown/PortfolioDropdown";
import "./PortfolioGrid.css";
import DateDropdown from "../DateDropdown/DateDropdown";
import type { Portfolio } from "../../types";
import fetchPortfolioBenchmarks from "../../api/FetchPortfolioBenchmark";

interface PortfolioGridProps {
  portfolio: Portfolio | null;
  setPortfolio: (portfolio: Portfolio | null) => void;
}

const PortfolioGrid = (props: PortfolioGridProps) => {
  const { portfolio, setPortfolio } = props;
  const { portfolioBenchmark, setPortfolioBenchmark } = usePortfolioBenchmark();

  async function fetchAndSetPortfolioBenchmark() {
    if (portfolio == null) {
      return;
    }
    const dtos = await fetchPortfolioBenchmarks(portfolio.portfolioId);
    setPortfolioBenchmark(dtos);
  }

  useEffect(() => {
    fetchAndSetPortfolioBenchmark();
  }, [portfolio]);

  const portfolioName = portfolioBenchmark
    ? portfolioBenchmark[0].portfolioName
    : "";

  const benchmarkName = portfolioBenchmark
    ? portfolioBenchmark[0].benchmarkName
    : "";

  function getPortfolioInfo(header: string, name: string) {
    return (
      <div className="subGridWrapper">
        <div className="labelHeader">{header}</div>
        <div className="labelName">{name}</div>
      </div>
    );
  }

  return (
    <div className="gridWrapper">
      <div className="cell" id="portfolioDropdown">
        <PortfolioPicker portfolio={portfolio} setPortfolio={setPortfolio} />
      </div>
      <div className="cell" id="dateDropdown">
        <DateDropdown />
      </div>
      <div className="cell portfolioInfo" id="portfolioLabel">
        {getPortfolioInfo("Selected Portfolio", portfolioName)}
      </div>
      <div className="cell portfolioInfo" id="benchmarkLabel">
        {getPortfolioInfo("Selected Benchmark", benchmarkName)}
      </div>
    </div>
  );
};

export default PortfolioGrid;
