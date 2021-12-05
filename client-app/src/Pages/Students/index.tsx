import LoadingWrapper from "@Components/LoadingWrapper";
import UpdateStudentModal from "@Components/Modals/UpdateStudent";
import StudentModel from "@Models/Student";
import * as MUI from "@mui/material";
import useStore from "@Stores";
import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";

import * as style from "./style";

function StudentsPage() {
  const { groupStore } = useStore();
  const { students, isLoading } = groupStore;

  useEffect(() => {
    groupStore.getStudents();
    return () => groupStore.dispose();
  }, [groupStore]);

  const [isOpenModal, setIsOpenModal] = useState(false);
  const toggleModal = () => setIsOpenModal(!isOpenModal);

  const [student, setStudent] = useState<StudentModel>();

  const onUpdateClick = (student: StudentModel) => {
    setStudent(student);
    toggleModal();
  };

  const updateCallback = (student: StudentModel) => {
    groupStore.updateStudent(student);
    toggleModal();
  };

  return (
    <LoadingWrapper size="10%" isLoading={isLoading} sx={style.loader}>
      <MUI.Box>
        <MUI.Box sx={style.wrapper}>
          <MUI.Table stickyHeader>
            <MUI.TableHead>
              <MUI.TableRow>
                <MUI.TableCell sx={style.tableHeaderCell}>
                  <MUI.Typography>Group</MUI.Typography>
                </MUI.TableCell>
                <MUI.TableCell align="center" sx={style.tableHeaderCell}>
                  <MUI.Typography>Last Name</MUI.Typography>
                </MUI.TableCell>
                <MUI.TableCell align="center" sx={style.tableHeaderCell}>
                  <MUI.Typography>First Name</MUI.Typography>
                </MUI.TableCell>
                <MUI.TableCell sx={style.tableHeaderCell}></MUI.TableCell>
              </MUI.TableRow>
            </MUI.TableHead>
            <MUI.TableBody>
              {students.map((s) => (
                <MUI.TableRow key={s.id}>
                  <MUI.TableCell sx={style.tableBodyCell}>
                    <MUI.Typography>{s.groupNumber}</MUI.Typography>
                  </MUI.TableCell>
                  <MUI.TableCell align="center" sx={style.tableBodyCell}>
                    <MUI.Typography>{s.lastName}</MUI.Typography>
                  </MUI.TableCell>
                  <MUI.TableCell align="center" sx={style.tableBodyCell}>
                    <MUI.Typography>{s.firstName}</MUI.Typography>
                  </MUI.TableCell>
                  <MUI.TableCell align="center" sx={style.buttonCell}>
                    <MUI.Button
                      size="small"
                      sx={style.updateButton}
                      onClick={() => onUpdateClick(s)}
                    >
                      Update
                    </MUI.Button>
                    <MUI.Button
                      size="small"
                      sx={style.deleteButton}
                      onClick={() => groupStore.deleteStudent(s.id)}
                    >
                      Delete
                    </MUI.Button>
                  </MUI.TableCell>
                </MUI.TableRow>
              ))}
            </MUI.TableBody>
          </MUI.Table>
        </MUI.Box>
      </MUI.Box>
      {isOpenModal && student && (
        <UpdateStudentModal
          isOpen={isOpenModal}
          handleClose={toggleModal}
          student={student}
          updateCallback={updateCallback}
        />
      )}
    </LoadingWrapper>
  );
}

export default observer(StudentsPage);
