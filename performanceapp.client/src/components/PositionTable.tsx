import { useState, useEffect } from "react";
import { usePortfolio } from "../contexts/PortfolioContext";
import { useBankday } from "../contexts/BankdayContext";
import "./Table.css";
import { api } from "../api/api";

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

  const getDateString = (date: Date | null) => {
    return date ? date.toISOString().split("T")[0] : "";
  };

  const fetchPositions = async () => {
    if (endpoint == null || portfolio == null || bankday == null) {
      return;
    }
    try {
      const portfolioId = portfolio.portfolioId;
      const dateString = getDateString(bankday);
      const url = `api/position/${endpoint}?portfolioId=${portfolioId}&date=${dateString}`;
      const response = await api(url);
      const data = await response.json();
      setPositions(data);
    } catch (err) {
      console.error(err);
    }
  };

  useEffect(() => {
    setPositions([]);
    fetchPositions();
  }, [portfolio, bankday, endpoint]);

  return (
    <table className="table table-striped" aria-labelledby="stockTableLabel">
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
