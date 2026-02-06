import PositionTable from "./PositionTable";
import type { BondPosition } from "../../types";
import { formatPercent, formatSEK } from "../../utilities/format";

const stockColumns = [
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

const BondTable = () => (
  <PositionTable<BondPosition> endpoint="bonds" columns={stockColumns} />
);

export default BondTable;
