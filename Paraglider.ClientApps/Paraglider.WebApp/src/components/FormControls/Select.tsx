import { SyntheticEvent } from "react";

import { Option, SelectRoot } from "./Common.styles";

interface Props<T> {
  nameKey: Extract<keyof T, string>;
  valueKey: Extract<keyof T, string>;
  items: T[];
  onChange: (e: SyntheticEvent) => void;
}

export const Select = <T extends object>(props: Props<T>) => {
  const { nameKey, valueKey, items, onChange } = props;

  return (
    <SelectRoot onChange={onChange}>
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
