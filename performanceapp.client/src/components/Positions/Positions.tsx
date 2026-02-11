import { useEffect, useState } from "react";
import type { BondPosition, Portfolio, StockPosition } from "../../types";
import BondTable from "./BondTable";
import StockTable from "./StockTable";
import fetchPositions from "../../api/FetchPositions";

interface PositionsProps {
  portfolio: Portfolio | null;
  date: Date | null;
}

export default function Positions({ portfolio, date }: PositionsProps) {
  const [stocks, setStocks] = useState<StockPosition[]>([]);
  const [bonds, setBonds] = useState<BondPosition[]>([]);

  useEffect(() => {
    async function fetchAndSetPositions() {
      const [_stocks, _bonds] = await Promise.all([
        fetchPositions("stocks", portfolio!.portfolioId, date!),
        fetchPositions("bonds", portfolio!.portfolioId, date!),
      ]);
      setStocks(_stocks);
      setBonds(_bonds);
    }
    if (portfolio && date) {
      fetchAndSetPositions();
    } else {
      setStocks([]);
      setBonds([]);
    }
  }, [portfolio, date]);

  if (!portfolio || !date) {
    return null;
  }

  return (
    <>
      <StockTable positions={stocks} />
      <BondTable positions={bonds} />
    </>
  );
}
