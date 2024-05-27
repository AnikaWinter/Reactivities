import { Grid } from "semantic-ui-react";
import ActivityList from "./ActivityList";
import { Activity } from "../../../app/models/activity";
import ActivityDetials from "../details/ActivityDetails";
import ActivityForm from "../form/ActivityForm";

interface Props {
  activities: Activity[];
  selectedActivity: Activity | undefined;
  selectActivity: (id: string) => void;
  cancelSelectActivity: () => void;
  editMode: boolean;
  openForm: (id: string) => void;
  closeForm: () => void;
  createOrEdit: (activity: Activity) => void;
  deleteActivity: (id: string) => void;
}

export default function ActivityDashboard({
  activities,
  selectedActivity,
  selectActivity,
  cancelSelectActivity,
  editMode,
  openForm,
  closeForm,
  createOrEdit,
  deleteActivity
}: Props) {
  //destructuring activity property itself from the props object
  return (
    <Grid>
      <Grid.Column width="10">
        <ActivityList activities={activities} 
            selectActivity={selectActivity} 
            deleteActivity={deleteActivity}
            />
      </Grid.Column>
      <Grid.Column width="6">
        {selectedActivity && !editMode &&  //only loads second part when left exists
          <ActivityDetials
            activity={selectedActivity}
            cancelSelectActivity={cancelSelectActivity}
            openForm={openForm}
          />
        }
        {editMode && 
        <ActivityForm closeForm={closeForm} activity={selectedActivity} createOrEdit={createOrEdit}/>}
      </Grid.Column>
    </Grid>
  );
}
