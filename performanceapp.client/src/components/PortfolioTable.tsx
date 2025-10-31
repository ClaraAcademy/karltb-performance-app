import { useState, useEffect } from "react";
import type { PortfolioBenchmarkDTO } from "../types";
import "./PortfolioTable.css"

interface PortfolioTableProps {
    portfolioId: number | undefined
}

const PortfolioTable: React.FC<PortfolioTableProps> = ({ portfolioId }) => {
    const [portfolioBenchmarkDTO, setPortfolioBenchmarkDTO] = useState<PortfolioBenchmarkDTO[]>([]);

    useEffect(() => {
        if (portfolioId === undefined) { return; }
        fetch(`api/PortfolioBenchmarkDTO/${portfolioId}`)
            .then((res) => {
                if (!res.ok) throw new Error(res.statusText);
                return res.json();
            })
            .then((data) => {
                console.log("Fetched portfolios and benchmarks:", data);
                setPortfolioBenchmarkDTO(data);
            })
            .catch((err) => console.error("Fetch error:", err));
    }, [portfolioId]);

    return (
        <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Portfolio Name</th>
                    <th>Benchmark Name</th>
                </tr>
            </thead>
            <tbody>
                {portfolioBenchmarkDTO.map(pb =>
                    <tr key={pb.portfolioId}>
                        <td>{pb.portfolioName}</td>
                        <td>{pb.benchmarkName}</td>
                    </tr>
                )}
            </tbody>
        </table>
    );
};
export default PortfolioTable;