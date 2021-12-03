import LoadingWrapper from "@Components/LoadingWrapper";
import * as MUI from "@mui/material";
import useStore from "@Stores";
import { observer } from "mobx-react-lite";
import React, { useEffect, useState } from "react";
import NumberFormat from "react-number-format";

import * as style from "./style";

interface Props {
  isOpen: boolean;
  handleClose: () => void;
  sendCallback: (groups: Array<string>, openPeriod?: number) => void;
}

function SurveySendingModal(props: Props) {
  const { isOpen, handleClose, sendCallback } = props;
  const { groupStore } = useStore();
  const { groupsNumbers, isLoading } = groupStore;

  useEffect(() => {
    groupStore.getGroupsNumbers();
  }, [groupStore]);

  const close = () => {
    setSelected([]);
    handleClose();
  };

  const [selected, setSelected] = useState<Array<string>>([]);

  const handleAllSelected = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.checked) {
      setSelected(groupsNumbers);
    } else {
      setSelected([]);
    }
  };

  const handleRowSelected = (event: React.ChangeEvent<HTMLInputElement>, group: string) => {
    if (event.target.checked) {
      setSelected(selected.concat(group));
    } else {
      setSelected(selected.filter((g) => g !== group));
    }
  };

  const checkSelection = (group: string) => selected.indexOf(group) !== -1;

  const [time, setTime] = useState<string>();

  const handleSend = () => {
    if (selected.length) {
      const period = time
        ?.split(":")
        .map((s) => parseInt(s.trim()))
        .reduceRight((prev, curr, i) => (prev || 0) + (curr * 60 ** (2 - i) || 0));
      sendCallback(selected, period);
      setSelected([]);
      handleClose();
    }
  };

  return (
    <MUI.Modal open={isOpen} onClose={close}>
      <MUI.Box sx={style.modal}>
        <MUI.Typography sx={style.title}>Select groups</MUI.Typography>
        <LoadingWrapper isLoading={isLoading} sx={style.loader} size={"15%"}>
          <MUI.Box sx={style.tableWrapper}>
            <MUI.Table stickyHeader padding="checkbox">
              <MUI.TableHead>
                <MUI.TableRow>
                  <MUI.TableCell sx={style.tableHeaderCell}>
                    <MUI.Checkbox sx={style.checkbox} onChange={handleAllSelected} />
                  </MUI.TableCell>
                  <MUI.TableCell sx={style.tableHeaderCell}>
                    <MUI.Typography align="center">Number</MUI.Typography>
                  </MUI.TableCell>
                </MUI.TableRow>
              </MUI.TableHead>
              <MUI.TableBody>
                {groupsNumbers.map((group) => (
                  <MUI.TableRow selected={checkSelection(group)} key={group}>
                    <MUI.TableCell sx={style.tableBodyCell}>
                      <MUI.Checkbox
                        sx={style.checkbox}
                        checked={checkSelection(group)}
                        onChange={(e) => handleRowSelected(e, group)}
                      />
                    </MUI.TableCell>
                    <MUI.TableCell align="center" sx={style.tableBodyCell}>
                      {group}
                    </MUI.TableCell>
                  </MUI.TableRow>
                ))}
              </MUI.TableBody>
            </MUI.Table>
          </MUI.Box>
        </LoadingWrapper>
        <NumberFormat
          customInput={PeriodInput}
          format="##:##:##"
          placeholder="hh:mm:ss"
          onChange={(e) => setTime(e.target.value)}
        />
        <MUI.Button sx={style.sendButton} onClick={handleSend} variant="outlined">
          Send
        </MUI.Button>
      </MUI.Box>
    </MUI.Modal>
  );
}

export default observer(SurveySendingModal);

const PeriodInput: React.FC<MUI.TextFieldProps> = (props: MUI.TextFieldProps) => (
  <style.TextInput {...props} label="OpenPeriod" />
);
