import { useCallback, useContext, useEffect, useState } from "react";

import { ApiContext } from "../../../../contexts";
import { City } from "../../../../typings/server";
import { Button } from "../../../Buttons";
import { BackButton } from "../../../Buttons/BackButton";
import { ControlsGroup, Input, Label } from "../../../FormControls";
import { Select } from "../../../FormControls";
import { CardBackButtonWrapper, CardRoot, CardTitle, TopRightImage } from "../../AuthCard.styles";
import { GirlIcon } from "../GirlIcon";
import { ButtonWrapper } from "./AccountInfo.styles";
import Circles from "./images/circles.svg";

interface AccountInfoProps {
  onGoBack: () => void;
}

export const AccountInfo = ({ onGoBack }: AccountInfoProps) => {
  const { citiesApi } = useContext(ApiContext);
  const [cities, setCities] = useState<City[]>([]);

  const fetchCities = useCallback(async () => {
    try {
      const response = await citiesApi.getCities();
      setCities(response.data);
    } catch (e) {
      console.error(e);
    }
  }, []);

  useEffect(() => {
    fetchCities();
  }, [fetchCities]);

  return (
    <CardRoot>
      <TopRightImage src={Circles} />

      <CardBackButtonWrapper>
        <BackButton onClick={onGoBack} />
      </CardBackButtonWrapper>

      <CardTitle>Регистрация</CardTitle>

      <GirlIcon />

      <ControlsGroup marginBottom={24}>
        <Label>Имя</Label>
        <Input type="text" />
      </ControlsGroup>

      <ControlsGroup marginBottom={24}>
        <Label>Фамилия</Label>
        <Input type="text" />
      </ControlsGroup>

      <ControlsGroup>
        <Label>Город</Label>
        <Select<City> nameKey="name" valueKey="id" items={cities} onChange={() => null} />
      </ControlsGroup>

      <ButtonWrapper>
        <Button variant="default" onClick={() => null}>
          Завершить
        </Button>
      </ButtonWrapper>
    </CardRoot>
  );
};
