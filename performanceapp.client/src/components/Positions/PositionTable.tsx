import "./Table.css";
import Table from "../Table/Table";
import type { PositionColumn } from "../../types";
import { createHeaderForPositions } from "../../Factories/HeaderFactory";
import { createRowsForPositions } from "../../Factories/RowFactory";

interface PositionTableProps<T> {
  name: string;
  columns: PositionColumn<T>[];
  positions: T[];
}

export default function PositionTable<T>(props: PositionTableProps<T>) {
  const { name, columns, positions } = props;

  const header = createHeaderForPositions(columns, name);
  const rows = createRowsForPositions(columns, positions);

  const headerID = name + "TableHeader";

  return (
    <>
      <h2 id={headerID}>{name}</h2>
      <Table header={header} rows={rows} />;
    </>
  );
}
