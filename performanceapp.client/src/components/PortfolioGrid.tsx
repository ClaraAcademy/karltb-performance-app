import { useEffect } from "react";
import { usePortfolioBenchmark } from "../contexts/PortfolioBenchmarkContext";
import { usePortfolio } from "../contexts/PortfolioContext";
import PortfolioDropdown from "./PortfolioDropdown";
import "./PortfolioGrid.css";
import DateDropdown from "./DateDropdown";

const PortfolioGrid = () => {
    const { portfolio } = usePortfolio();
    const { portfolioBenchmark, setPortfolioBenchmark } =
        usePortfolioBenchmark();
    useEffect(() => {
        if (portfolio == null) {
            return;
        }
        fetch(`api/PortfolioBenchmark?portfolioId=${portfolio.portfolioId}`)
            .then((res) => {
                if (!res.ok) throw new Error(res.statusText);
                return res.json();
            })
            .then((data) => {
                console.log("Fetched portfolios and benchmarks:", data);
                setPortfolioBenchmark(data);
            })
            .catch((err) => console.error("Fetch error:", err));
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
                {getPortfolioInfo("Selected Portfolio:", portfolioName)}
            </div>
            <div className="cell portfolioInfo" id="benchmarkLabel">
                {getPortfolioInfo("Selected Benchmark:", benchmarkName)}
            </div>
        </div>
    );
};

export default PortfolioGrid;
