import PositionTable from "./PositionTable";
import type { BondPosition, PositionColumn } from "../../types";
import { formatPercent, formatSEK } from "../../utilities/format";

const stockColumns: PositionColumn<BondPosition>[] = [
  {
    header: "Name",
    className: "tdText",
    accessor: (p: BondPosition) => p.instrumentName,
  },
  {
    header: "Nominal Value",
    className: "tdNumber",
    accessor: (p: BondPosition) => formatSEK(p.nominal),
  },
  {
    header: "Daily Price (%)",
    className: "tdNumber",
    accessor: (p: BondPosition) => formatPercent(p.unitPrice),
  },
  {
    header: "Value",
    className: "tdNumber",
    accessor: (p: BondPosition) => formatSEK(p.value),
  },
];

interface BondTableProps {
  positions: BondPosition[];
}

export default function BondTable({ positions }: BondTableProps) {
  return (
    <PositionTable<BondPosition>
      name={"bond"}
      columns={stockColumns}
      positions={positions}
    />
  );
}
