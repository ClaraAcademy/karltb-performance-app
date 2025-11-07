import PositionTable from "./PositionTable";
import type { BondPosition } from "../types";
import { formatPercent, formatSEK } from "../utilities/format";

const stockColumns = [
    { header: "Name", accessor: (p: BondPosition) => p.instrumentName },
    {
        header: "Nominal Value",
        accessor: (p: BondPosition) => formatSEK(p.nominal),
    },
    {
        header: "Daily Price (%)",
        accessor: (p: BondPosition) => formatPercent(p.unitPrice),
    },
    { header: "Value", accessor: (p: BondPosition) => formatSEK(p.value) },
];

const BondTable = () => (
    <PositionTable<BondPosition> endpoint="bonds" columns={stockColumns} />
);

export default BondTable;
