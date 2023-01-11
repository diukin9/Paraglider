import { SyntheticEvent } from "react";

import { Option, SelectRoot } from "./Common.styles";

interface Props<T> {
  nameKey: Extract<keyof T, string>;
  valueKey: Extract<keyof T, string>;
  items: T[];
  value: string;
  onChange: (e: SyntheticEvent) => void;
  placeholder?: string;
  name?: string;
}

export const Select = <T extends object>(props: Props<T>) => {
  const { nameKey, valueKey, items, value, onChange, name, placeholder } = props;

  return (
    <SelectRoot name={name} value={value} onChange={onChange}>
      {placeholder && (
        <Option value="" disabled>
          {placeholder}
        </Option>
      )}
      {items.map((item) => {
        const value = item[valueKey];
        const name = item[nameKey];

        if (typeof value === "string" && typeof name === "string") {
          return (
            <Option value={value} key={value}>
              {name}
            </Option>
          );
        }
      })}
    </SelectRoot>
  );
};
