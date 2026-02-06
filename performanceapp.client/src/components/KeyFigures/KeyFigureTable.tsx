import { useState, useEffect } from "react";
import { usePortfolio } from "../../contexts/PortfolioContext";
import { usePortfolioBenchmark } from "../../contexts/PortfolioBenchmarkContext";
import "./KeyFigureTable.css";
import { type Row, type PortfolioBenchmarkKeyFigure } from "../../types";
import { createHeader } from "../../Factories/HeaderFactory";
import { createRows } from "../../Factories/RowFactory";
import Table from "../Table/Table";
import fetchKeyFigures from "../../api/FetchKeyFigures";

function KeyFigureTable() {
  const [keyFigures, setKeyFigures] = useState<PortfolioBenchmarkKeyFigure[]>(
    [],
  );
  const { portfolio } = usePortfolio();
  const { portfolioBenchmark } = usePortfolioBenchmark();
  const [header, setHeader] = useState<Row>();
  const [rows, setRows] = useState<Row[]>([]);

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
    async function fetchAndSet() {
      const kfs = await fetchKeyFigures(portfolio!.portfolioId);
      setKeyFigures(kfs);
    }
    fetchAndSet();
  }, [portfolio, portfolioBenchmark]);

  useEffect(() => {
    if (!portfolioBenchmark) {
      return;
    }
    const _header = createHeader(portfolioBenchmark[0]);
    setHeader(_header);
  }, [portfolioBenchmark]);

  useEffect(() => {
    if (!keyFigures) {
      return;
    }
    const _rows = createRows(keyFigures);
    setRows(_rows);
  }, [keyFigures]);

  return isLoaded() && header && rows ? (
    <Table header={header} rows={rows} />
  ) : (
    <></>
  );
}

export default KeyFigureTable;
