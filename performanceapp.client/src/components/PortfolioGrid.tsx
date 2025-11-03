import { useEffect } from "react";
import { usePortfolioBenchmark } from "../contexts/PortfolioBenchmarkContext";
import { usePortfolio } from "../contexts/PortfolioContext";
import PortfolioDropdown from "./PortfolioDropdown";
import "./PortfolioGrid.css";

const PortfolioGrid = () => {
    const { portfolio } = usePortfolio();
    const { portfolioBenchmark, setPortfolioBenchmark } =
        usePortfolioBenchmark();
    useEffect(() => {
        if (portfolio == null) {
            return;
        }
        fetch(`api/PortfolioBenchmarkDto/${portfolio.portfolioId}`)
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
    return (
        <div className="parent">
            <div id="div1">
                <PortfolioDropdown />
            </div>
            <div className="labelHeader" id="div2">
                Selected Portfolio:
            </div>
            <div className="labelHeader" id="div3">
                Selected Benchmark:
            </div>
            <div className="labelName" id="div4">
                {portfolioBenchmark ? portfolioBenchmark[0].portfolioName : ""}
            </div>
            <div className="labelName" id="div5">
                {portfolioBenchmark ? portfolioBenchmark[0].benchmarkName : ""}
            </div>
        </div>
    );
};

export default PortfolioGrid;
