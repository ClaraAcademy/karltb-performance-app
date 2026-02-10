import PositionTable from "./PositionTable";
import type { PositionColumn, StockPosition } from "../../types";
import { formatSEK, formatInt } from "../../utilities/format";

const stockColumns: PositionColumn<StockPosition>[] = [
  {
    header: "Name",
    className: "tdText",
    accessor: (p: StockPosition) => p.instrumentName,
  },
  {
    header: "Count",
    className: "tdNumber",
    accessor: (p: StockPosition) => formatInt(p.count),
  },
  {
    header: "Daily Price",
    className: "tdNumber",
    accessor: (p: StockPosition) => formatSEK(p.unitPrice),
  },
  {
    header: "Value",
    className: "tdNumber",
    accessor: (p: StockPosition) => formatSEK(p.value),
  },
];

interface StockTableProps {
  positions: StockPosition[];
}

export default function StockTable({ positions }: StockTableProps) {
  return (
    <PositionTable<StockPosition>
      name={"stock"}
      columns={stockColumns}
      positions={positions}
    />
  );
}
