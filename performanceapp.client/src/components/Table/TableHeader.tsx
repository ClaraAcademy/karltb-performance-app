import type { Row } from "../../types";

interface TableHeaderProps {
  header: Row;
}

export default function TableHeader({ header }: TableHeaderProps) {
  return (
    <thead>
      <tr key={header.key}>
        {header.values.map((kv) => (
          <th key={kv.key} className={kv.key}>
            {kv.value}
          </th>
        ))}
      </tr>
    </thead>
  );
}
