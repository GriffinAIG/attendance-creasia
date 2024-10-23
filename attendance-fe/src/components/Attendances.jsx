import React, { useState, useEffect } from 'react';
import { Table, Spin, message } from 'antd';
import axios from 'axios';

const Attendance = () => {
    const [attendances, setAttendances] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        getDataEmployees()
    }, []);

    const getDataEmployees = () => {
        axios.get('http://localhost:5013/api/Attendance/attendance/all')
            .then(response => {
                setAttendances(response.data);
                setLoading(false);
            })
            .catch(error => {
                setError(error.message);
                message.error('Failed to load attendance data');
                setLoading(false);
            });
    }

    const columns = [
        {
            title: 'Employee ID',
            dataIndex: 'EmployeeId',
            key: 'EmployeeId',
        },
        {
            title: 'Name',
            dataIndex: 'EmployeeName',
            key: 'EmployeeName',
        },
        {
            title: 'Date',
            dataIndex: 'AttendanceDate',
            key: 'AttendanceDate',
            render: (text) => new Date(text).toLocaleDateString(),
        },
        {
            title: 'Check-In Time',
            dataIndex: 'CheckInTime',
            key: 'CheckInTime',
        },
        {
            title: 'Check-Out Time',
            dataIndex: 'CheckOutTime',
            key: 'CheckOutTime',
        },
    ];

    if (loading) {
        return <Spin tip="Loading..." />;
    }


    if (error) {
        return <p className="text-red-500">Error: {error}</p>;
    }

    return (
        <div className="container mx-auto p-4">
            <h1 className="text-2xl font-bold mb-4 text-center">Employee Attendance</h1>
            <Table
                className="shadow-md rounded-lg overflow-hidden"
                columns={columns}
                dataSource={attendances}
                rowKey="Id"
                pagination={{ pageSize: 10 }}
            />
        </div>
    );
}

export default Attendance;