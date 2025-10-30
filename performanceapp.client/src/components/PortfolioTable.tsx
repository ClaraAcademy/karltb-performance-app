import { useState, useEffect } from "react";
import type { PortfolioBenchmarkDTO } from "../types";

interface PortfolioTableProps {
    portfolioID: number | undefined
}

const PortfolioTable: React.FC<PortfolioTableProps> = ({ portfolioID }) => {
    const [portfolioBenchmarkDTO, setPortfolioBenchmarkDTO] = useState<PortfolioBenchmarkDTO[]>([]);

    useEffect(() => {
        if (portfolioID === undefined) { return; }
        fetch(`api/PortfolioBenchmarkDTO/${portfolioID}`)
            .then((res) => {
                if (!res.ok) throw new Error(res.statusText);
                return res.json();
            })
            .then((data) => {
                console.log("Fetched portfolios and benchmarks:", data);
                setPortfolioBenchmarkDTO(data);
            })
            .catch((err) => console.error("Fetch error:", err));
    }, [portfolioID]);

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
                    <tr key={pb.portfolioID}>
                        <td>{pb.portfolioName}</td>
                        <td>{pb.benchmarkName}</td>
                    </tr>
                )}
            </tbody>
        </table>
    );
};
export default PortfolioTable;