import { useState, useEffect } from "react";
import { usePortfolio } from "../contexts/PortfolioContext";
import { useBankday } from "../contexts/BankdayContext";
import "./Table.css"

interface PositionTableProps<T> {
    endpoint: string | undefined;
    columns: {
        header: string;
        className: string;
        accessor: (row: T) => React.ReactNode;
    }[];
}

function PositionTable<T>({ endpoint, columns }: PositionTableProps<T>) {
    const { portfolio } = usePortfolio();
    const { bankday } = useBankday();
    const [positions, setPositions] = useState<T[]>([]);

    useEffect(() => {
        setPositions([]);
        if (endpoint == null || portfolio == null || bankday == null) {
            return;
        }
        const portfolioId = portfolio.portfolioId;
        const dateString = bankday ? bankday.toISOString().split("T")[0] : "";
        fetch(
            `api/position/${endpoint}?portfolioId=${portfolioId}&date=${dateString}`,
        )
            .then((res) => {
                if (!res.ok) throw new Error(res.statusText);
                return res.json();
            })
            .then((data) => {
                console.log(`Fetched ${endpoint} positions: `, data);
                setPositions(data);
            })
            .catch((err) => console.error("Fetch error: ", err));
    }, [portfolio, bankday, endpoint]);

    return (
        <table
            className="table table-striped"
            aria-labelledby="stockTableLabel"
        >
            <thead>
                <tr>
                    {columns.map((col, idx) => (
                        <th key={idx}>{col.header}</th>
                    ))}
                </tr>
            </thead>
            <tbody>
                {positions.map((row: T, idx) => (
                    <tr key={idx}>
                        {columns.map((col, cidx) => (
                            <td key={cidx} className={col.className}>
                                {col.accessor(row)}
                            </td>
                        ))}
                    </tr>
                ))}
            </tbody>
        </table>
    );
}

export default PositionTable;
