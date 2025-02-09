import axios from "axios";

// יצירת מופע axios עם הגדרות קבועות
const axiosInstance = axios.create({
  baseURL: process.env.REACT_APP_API_URL, // כתובת השרת שלך
  timeout: 10000, // זמן סיום בקשה (במילישניות)
  headers: { "Content-Type": "application/json" },
});

export default {
  getTasks: async () => {
    try {
      const result = await axiosInstance.get("/items");
      return result.data;
    } catch (error) {
      console.error("Error in getTasks:", error.message);
      return [];
    }
  },

  // פונקציה להוספת משימה חדשה
  addTask: async (name) => {
    try {
      const result = await axiosInstance.post("/items", { name, isComplete: false });
      console.log("addTask", result.data);
      return result.data;
    } catch (error) {
      console.error("Error in addTask:", error.message);
      return {};
    }
  },

  // פונקציה לעדכון סטטוס משימה
  setCompleted: async (id, isComplete) => {
    try {
      const result = await axiosInstance.put(`/items/${id}`, { isComplete });
      console.log("setCompleted", { id, isComplete });
      return result.data;
    } catch (error) {
      console.error("Error in setCompleted:", error.message);
      throw error; // חשוב לזרוק את השגיאה כדי שתוכל לתפוס אותה במקום אחר אם יש צורך
    }
  },

  // פונקציה למחיקת משימה לפי מזהה
  deleteTask: async (id) => {
    try {
      await axiosInstance.delete(`/items/${id}`);
      console.log("deleteTask", id);
      return {};
    } catch (error) {
      console.error("Error in deleteTask:", error.message);
      return {};
    }
  },
};


