import { useState, useEffect } from "react";

interface PositionTableProps<T> {
    portfolioId: number | undefined;
    bankday: Date | undefined;
    endpoint: string | undefined;
    columns: { header: string; accessor: (row: T) => React.ReactNode }[];
}

function PositionTable<T>({
    portfolioId,
    bankday,
    endpoint,
    columns,
}: PositionTableProps<T>) {
    const [positions, setPositions] = useState<T[]>([]);
    useEffect(() => {
        setPositions([])
        if (endpoint == null || portfolioId == null || bankday == null) {
            return;
        }
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
    }, [portfolioId, bankday, endpoint]);

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
                            <td key={cidx}>{col.accessor(row)}</td>
                        ))}
                    </tr>
                ))}
            </tbody>
        </table>
    );
}

export default PositionTable;
