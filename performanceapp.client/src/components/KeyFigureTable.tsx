import { useState, useEffect } from "react";
import { usePortfolio } from "../contexts/PortfolioContext";
import { usePortfolioBenchmark } from "../contexts/PortfolioBenchmarkContext";
import { formatPercent } from "../utilities/format";
import "./KeyFigureTable.css";
import type { PortfolioBenchmarkKeyFigure } from "../types";

function KeyFigureTable() {
    const [portfolioBenchmarkKeyFigures, setPortfolioBenchmarkKeyFigures] =
        useState<PortfolioBenchmarkKeyFigure[]>([]);
    const { portfolio } = usePortfolio();
    const { portfolioBenchmark } = usePortfolioBenchmark();

    function isLoaded(): boolean {
        return (
            portfolio != null &&
            portfolioBenchmark != null &&
            portfolioBenchmark.length > 0
        );
    }

    useEffect(() => {
        if (!isLoaded()) {
            return;
        }

        fetch(`api/performance?portfolioId=${portfolio?.portfolioId}`)
            .then((res) => res.json())
            .then((data) => {
                console.log("Fetched portfolioBenchmarkKeyFigure: " + data);
                setPortfolioBenchmarkKeyFigures(data);
            })
            .catch((err) => console.error(err));
    }, [portfolio, portfolioBenchmark]);

    return !isLoaded() ? (
        <div />
    ) : (
        <table
            className="table table-striped"
            aria-labelledby="keyFigureTableLabel"
        >
            <thead>
                <tr>
                    <th key="keyFigureName">Key Figure</th>
                    <th key={portfolioBenchmark![0].portfolioId}>
                        {portfolioBenchmark![0].portfolioName}
                    </th>
                    <th key={portfolioBenchmark![0].benchmarkId}>
                        {portfolioBenchmark![0].benchmarkName}
                    </th>
                </tr>
            </thead>
            <tbody>
                {portfolioBenchmarkKeyFigures.map(
                    (kf: PortfolioBenchmarkKeyFigure) => (
                        <tr key={kf.keyFigureId}>
                            <td>{kf.keyFigureName}</td>
                            <td>{formatPercent(kf.portfolioValue)}</td>
                            <td>{formatPercent(kf.benchmarkValue)}</td>
                        </tr>
                    ),
                )}
            </tbody>
        </table>
    );
}

export default KeyFigureTable;
