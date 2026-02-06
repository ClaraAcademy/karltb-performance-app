import { useEffect } from "react";
import { usePortfolioBenchmark } from "../../contexts/PortfolioBenchmarkContext";
import { usePortfolio } from "../../contexts/PortfolioContext";
import PortfolioDropdown from "../PortfolioDropdown/PortfolioDropdown";
import "./PortfolioGrid.css";
import DateDropdown from "../DateDropdown/DateDropdown";
import { api } from "../../api/api";

const PortfolioGrid = () => {
  const { portfolio } = usePortfolio();
  const { portfolioBenchmark, setPortfolioBenchmark } = usePortfolioBenchmark();

  const fetchPortfolioBenchmark = async () => {
    try {
      const endpoint = `api/PortfolioBenchmark?portfolioId=${portfolio?.portfolioId}`;
      const response = await api(endpoint);
      const data = await response.json();
      setPortfolioBenchmark(data);
    } catch (err) {
      console.error(err);
    }
  };

  useEffect(() => {
    if (portfolio == null) {
      return;
    }
    fetchPortfolioBenchmark();
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
        <PortfolioDropdown />
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
