import { useFormikContext } from "formik";
import { useCallback, useContext, useEffect, useState } from "react";

import { ApiContext } from "../../../../contexts";
import { City } from "../../../../typings/server";
import { Button } from "../../../Buttons";
import { BackButton } from "../../../Buttons/BackButton";
import { ControlsGroup, Input, Label } from "../../../FormControls";
import { Select } from "../../../FormControls";
import { CardBackButtonWrapper, CardRoot, CardTitle, TopRightImage } from "../../AuthCard.styles";
import { GirlIcon } from "../GirlIcon";
import { RegistrationForm } from "../RegistrationCard.helpers";
import { ButtonWrapper } from "./AccountInfo.styles";
import Circles from "./images/circles.svg";

interface AccountInfoProps {
  onGoBack: () => void;
}

export const AccountInfo = ({ onGoBack }: AccountInfoProps) => {
  const { values, handleChange, handleSubmit } = useFormikContext<RegistrationForm>();
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
        <Input name="firstName" type="text" value={values.firstName} onChange={handleChange} />
      </ControlsGroup>

      <ControlsGroup marginBottom={24}>
        <Label>Фамилия</Label>
        <Input name="surname" type="text" value={values.surname} onChange={handleChange} />
      </ControlsGroup>

      <ControlsGroup>
        <Label>Город</Label>
        <Select<City>
          name="cityId"
          nameKey="name"
          valueKey="id"
          items={cities}
          value={values.cityId}
          onChange={handleChange}
          placeholder="Выберите город"
        />
      </ControlsGroup>

      <ButtonWrapper>
        <Button variant="default" onClick={handleSubmit}>
          Завершить
        </Button>
      </ButtonWrapper>
    </CardRoot>
  );
};
