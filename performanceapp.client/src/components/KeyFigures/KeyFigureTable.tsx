import { useState, useEffect } from "react";
import "./KeyFigureTable.css";
import { type PortfolioBenchmarkKeyFigure, type Portfolio } from "../../types";
import { createHeaderForPortfolioAndBenchmark } from "../../Factories/HeaderFactory";
import { createRows } from "../../Factories/RowFactory";
import Table from "../Table/Table";
import { fetchAndSetKeyFigures } from "../../api/FetchKeyFigures";

interface KeyFigureTableProps {
  portfolio: Portfolio | null;
  benchmark: Portfolio | null;
}

export default function KeyFigureTable({
  portfolio,
  benchmark,
}: KeyFigureTableProps) {
  const [keyFigures, setKeyFigures] = useState<PortfolioBenchmarkKeyFigure[]>(
    [],
  );

  useEffect(() => {
    if (!portfolio) {
      setKeyFigures([]);
    } else {
      fetchAndSetKeyFigures(portfolio.portfolioId, setKeyFigures);
    }
  }, [portfolio]);

  if (!portfolio || !benchmark || keyFigures.length === 0) {
    return null;
  }

  const header = createHeaderForPortfolioAndBenchmark(portfolio, benchmark);
  const rows = createRows(keyFigures);

  return <Table header={header} rows={rows} />;
}
