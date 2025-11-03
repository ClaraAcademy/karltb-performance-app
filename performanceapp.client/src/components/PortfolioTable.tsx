import { useState, useEffect } from "react";
import "./PortfolioTable.css";
import { usePortfolioBenchmark } from "../contexts/PortfolioBenchmarkContext";
import { usePortfolio } from "../contexts/PortfolioContext";

const PortfolioTable = () => {
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
        <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Portfolio Name</th>
                    <th>Benchmark Name</th>
                </tr>
            </thead>
            <tbody>
                {portfolioBenchmark?.map((pb) => (
                    <tr key={pb?.portfolioId}>
                        <td>{pb?.portfolioName}</td>
                        <td>{pb?.benchmarkName}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
};
export default PortfolioTable;
