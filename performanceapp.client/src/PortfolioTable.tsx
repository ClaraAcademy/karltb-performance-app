import { useState, useEffect } from "react";

interface PortfolioBenchmarkDTO {
    portfolioID: number;
    portfolioName: string;
    benchmarkID: number;
    benchmarkName: string;
}
const PortfolioTable = () => {
    const [portfolioBenchmarkDTO, setPortfolioBenchmarkDTO] = useState<PortfolioBenchmarkDTO[]>([]);

    useEffect(() => {
        fetch("api/PortfolioBenchmarkDTO")
            .then((res) => {
                if (!res.ok) throw new Error(res.statusText);
                return res.json();
            })
            .then((data) => {
                console.log("Fetched portfolios and benchmarks:", data);
                setPortfolioBenchmarkDTO(data);
            })
            .catch((err) => console.error("Fetch error:", err));

    }, []);

    return (
        <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Portfolio ID</th>
                    <th>Portfolio Name</th>
                    <th>Benchmark ID</th>
                    <th>Benchmark Name</th>
                </tr>
            </thead>
            <tbody>
                {portfolioBenchmarkDTO.map(pb =>
                    <tr key={pb.portfolioID}>
                        <td>{pb.portfolioID}</td>
                        <td>{pb.portfolioName}</td>
                        <td>{pb.benchmarkID}</td>
                        <td>{pb.benchmarkName}</td>
                    </tr>
                )}
            </tbody>
        </table>
    );
};
export default PortfolioTable;