import { useEffect, useState } from "react";
import type { BondPosition, Portfolio, StockPosition } from "../../types";
import BondTable from "./BondTable";
import StockTable from "./StockTable";
import fetchPositions from "../../api/FetchPositions";

interface PositionsProps {
  portfolio: Portfolio;
  bankday: Date;
}

export default function Positions(props: PositionsProps) {
  const { portfolio, bankday } = props;
  const [stocks, setStocks] = useState<StockPosition[]>([]);
  const [bonds, setBonds] = useState<BondPosition[]>([]);

  useEffect(() => {
    async function fetchAndSetPositions() {
      const [_stocks, _bonds] = await Promise.all([
        fetchPositions("stocks", portfolio.portfolioId, bankday),
        fetchPositions("bonds", portfolio.portfolioId, bankday),
      ]);
      setStocks(_stocks);
      setBonds(_bonds);
    }
    fetchAndSetPositions();
  }, [portfolio, bankday]);

  return (
    <>
      <StockTable positions={stocks} />
      <BondTable positions={bonds} />
    </>
  );
}
