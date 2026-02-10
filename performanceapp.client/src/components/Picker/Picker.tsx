import type { KeyValue, OnChange } from "../../types";

interface PickerProps {
  selected: string;
  onChange: OnChange;
  values: KeyValue<string, string>[];
}

export default function Picker(props: PickerProps) {
  const { selected, onChange, values } = props;
  return (
    <select value={selected} onChange={onChange}>
      {values.map((p) => (
        <option key={p.key} value={p.key}>
          {p.value}
        </option>
      ))}
    </select>
  );
}
